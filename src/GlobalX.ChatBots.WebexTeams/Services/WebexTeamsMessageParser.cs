using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoMapper;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsMessageParser : IWebexTeamsMessageParser
    {
        private const string PersonIdPrefix = "ciscospark://us/PEOPLE/";
        private const string RoomIdPrefix = "ciscospark://us/ROOM/";
        private const string GroupMention = "group";
        private const string PersonMention = "person";

        private readonly IMapper _mapper;

        public WebexTeamsMessageParser(IWebexTeamsMapper mapper)
        {
            _mapper = mapper;
        }

        public GlobalXMessage ParseMessage(WebexTeamsMessage message)
        {
            var mappedMessage = _mapper.Map<GlobalXMessage>(message);

            if (mappedMessage.RoomType == RoomType.Direct)
            {
                mappedMessage.MessageParts = new[]
                {
                    new MessagePart
                    {
                        MessageType = MessageType.Text,
                        Text = message.Text
                    }
                };
            }
            else
            {
                var xmlDocument = TryParseXml(message.Html);
                var parts = new List<MessagePart>();
                foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
                {
                    parts.Add(ParseMessagePart(node));
                }
            }

            return mappedMessage;
        }

        public CreateMessageRequest ParseCreateMessageRequest(GlobalXMessage message)
        {
            var request = new CreateMessageRequest();
            var roomBytes = Convert.FromBase64String(message.RoomId);
            var roomDecoded = Encoding.UTF8.GetString(roomBytes);

            if (roomDecoded.StartsWith(RoomIdPrefix))
            {
                request.RoomId = message.RoomId;
            }
            else if (roomDecoded.StartsWith(PersonIdPrefix))
            {
                request.ToPersonId = message.RoomId;
            }
            else
            {
                throw new ArgumentException($"Invalid room ID {message.RoomId}");
            }

            if (!string.IsNullOrWhiteSpace(message.Text) && (message.MessageParts == null || message.MessageParts.Length == 0))
            {
                request.Markdown = message.Text;
            }
            else if (message.MessageParts?.Length > 0)
            {
                var markdownBuilder = new StringBuilder();
                foreach (MessagePart messagePart in message.MessageParts)
                {
                    switch (messagePart.MessageType)
                    {
                        case MessageType.Text:
                            markdownBuilder.Append(messagePart.Text);
                            break;
                        case MessageType.PersonMention:
                            markdownBuilder.Append($"<@personId:{messagePart.UserId}|{messagePart.Text}>");
                            break;
                        case MessageType.GroupMention:
                            markdownBuilder.Append($"<@{messagePart.UserId}>");
                            break;
                        default:
                            throw new ArgumentException($"Invalid message type {messagePart.MessageType}");
                    }
                }

                request.Markdown = markdownBuilder.ToString();
            }
            else
            {
                throw new ArgumentException("Please provide a message to send");
            }

            return request;
        }

        private XmlDocument TryParseXml(string xml)
        {
            bool success = false;
            var document = new XmlDocument();

            try
            {
                document.LoadXml(xml);
                success = true;
            }
            catch (XmlException)
            {
                // The xml might not always have a root node. If it fails, add one and try again
                xml = $"<p>{xml}</p>";
            }

            if (!success)
            {
                document.LoadXml(xml);
            }

            return document;
        }

        private MessagePart ParseMessagePart(XmlNode node)
        {
            if (node.HasChildNodes)
            {
                throw new ArgumentException("Html has more levels than expected");
            }

            var part = new MessagePart();

            if (node.Attributes["data-object-type"] != null)
            {
                string type = node.Attributes["data-object-type"].InnerText;
                if (type == GroupMention)
                {
                    part.MessageType = MessageType.GroupMention;
                    part.UserId = node.Attributes["data-group-type"]?.InnerText;
                }
                else if (type == PersonMention)
                {
                    part.MessageType = MessageType.PersonMention;
                    var userId = node.Attributes["data-object-id"]?.InnerText;

                    if (userId == null)
                    {
                        throw new ArgumentException("Mention has no associated user id");
                    }

                    part.UserId = ParseUserId(userId);
                }
            }
            else
            {
                part.MessageType = MessageType.Text;
            }

            part.Text = node.InnerText;
            return part;
        }

        private string ParseUserId(string userId)
        {
            if (userId.Contains('-'))
            {
                var bytes = Encoding.UTF8.GetBytes($"{PersonIdPrefix}{userId}");
                return Convert.ToBase64String(bytes);
            }

            return userId;
        }
    }
}
