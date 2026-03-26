import { useEffect, useState, useRef } from "react";
import { useParams } from 'react-router-dom';
import { loadUser, getChatMessages } from '../../services/requests';
import sendIcon from '../../assets/send-icon.svg';
import * as signalR from "@microsoft/signalr";

function ChatMessages() {
    const { chatId } = useParams();
    const [user, setUser] = useState(null);
    const [messages, setMessages] = useState([]);
    const [mContent, setMContent] = useState("");

    const connectionRef = useRef(null);

    const handleMessageChange = (event) => {
        setMContent(event.target.value);
    };

    const findMessageById = (refId) => {
        const foundMessage = messages.find(m => m.id === refId);
        return foundMessage ? foundMessage.content.slice(0, 20)+"..." : null;
    }

    const sendMessage = async () => {
        if (!mContent.trim()) return;
        const connection = connectionRef.current;
        if (!connection || connection.state !== signalR.HubConnectionState.Connected) {
            console.error("SignalR not connected");
            return;
        };

        //crypto.randomUUID works only for localhost or https
        const uuid = (typeof crypto.randomUUID === 'function') 
            ? crypto.randomUUID() 
            : URL.createObjectURL(new Blob([])).slice(-36);

        const tempMessage = {
            id: uuid,
            content: mContent,
            userId: user.id,
            timeStamp: new Date().toISOString(),
            isTemp: true
        };
        setMessages(prev => [...prev, tempMessage]);
        setMContent("");
        try {
            await connectionRef.current.invoke("SendMessage", {
                chatId,
                id: tempMessage.id,
                userId: tempMessage.userId, 
                content: tempMessage.content,
                timeStamp: tempMessage.timeStamp
            });
        } catch (err) {
            console.error(err);
        }
    };

    useEffect(() => {
        loadUser(setUser);

        const loadMessages = async () => {
            const loadedMessages = await getChatMessages(chatId ,1, 20);
            setMessages(loadedMessages.data);
        };

        const connectSignalR = async () => {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("http://192.168.100.5:5194/chatHub", {
                    withCredentials: true
                })
                .withAutomaticReconnect()
                .build();
            connection.on("ReceiveMessage", (message) => {
                setMessages(prev => {
                    const withoutTemp = prev.filter(m => !m.isTemp);
                    return [...withoutTemp, message];
                });
            });
            await connection.start();
            await connection.invoke("JoinChat", chatId);
            connectionRef.current = connection;
        };
        loadMessages();
        connectSignalR();
        return () => {
            connectionRef.current?.stop();
        };
    }, [chatId]);

    return (
        <div>
            {messages.map(message => (
                <div key={message.id} className="my-4">
                    {message.messageReferenceId ? 
                    <div className="bg-gray-700 text-gray-300 rounded-xl w-[80%]">
                        {findMessageById(message.messageReferenceId)}
                    </div> : <></>}
                    <div  className="bg-gray-100 text-black p-2 rounded-xl">                
                        <span>
                            User #{message.userId}:
                        </span>
                        <span>
                            {message.content}
                        </span>
                    </div>
                    <div className="text-xs">{new Date(message.timeStamp).toLocaleString('ru-RU')}</div>
                </div>
            ))}
            <div className="flex">
                <input
                    name="messageField"
                    type="text"
                    value={mContent}
                    onChange={handleMessageChange}
                    className="bg-white text-black rounded-xl mx-2"
                />
                <div className="w-[3vw] bg-white rounded-[50%] p-1 cursor-pointer hover:bg-gray-200">
                    <a onClick={() => sendMessage()}><img src={sendIcon} alt="send" /></a>
                </div>
            </div>
        </div>
    )
}

export default ChatMessages;