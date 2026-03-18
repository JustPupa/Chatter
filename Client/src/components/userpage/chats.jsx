import React from "react";
import { useEffect, useState } from "react";
import { getUserChats } from '../../services/requests';

function Chats() {
    const [chats, setChats] = useState([]);

    useEffect(() => {
    const loadChats = async () => {
      const chatsData = await getUserChats(1, 15);
      console.log(chatsData);
      setChats(chatsData.data);
    };
    loadChats();
  }, []);

  return (
    <div>
      <span className="fixed top-5 left-5 flex items-center justify-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all">
        My Chats
      </span>
      {chats.map(chat => (
        <div key={chat.id}>
          {chat.name}
        </div>
      ))}
    </div>
  )
}

export default Chats;