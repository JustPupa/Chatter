import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';
import { getChatMessages } from '../../services/requests';

function ChatMessages() {
    const { chatId } = useParams();

    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const loadMessages = async () => {
            const messageData = await getChatMessages(chatId ,1, 20);
            console.log(messageData);
            setMessages(messageData.data);
        };
        loadMessages();
    }, [chatId]);

    return (
        <div>
            {messages.map(message => (
                <div key={message.id}>                
                    <span>
                        {message.userid}
                    </span>
                    <span>
                        {message.content}
                    </span>
                </div>
            ))}
        </div>
    )
}

export default ChatMessages;