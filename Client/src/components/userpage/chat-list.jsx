import { useEffect, useState } from "react";
import { Outlet, NavLink } from "react-router-dom";
import { getUserChats } from '../../services/requests';

function ChatList() {
    const [chats, setChats] = useState([]);

    useEffect(() => {
    const loadChats = async () => {
      const chatsData = await getUserChats(1, 15);
      setChats(chatsData.data);
    };
    loadChats();
  }, []);

  return (
    <>
    <div className="flex flex-col items-start">
      {chats.map(chat => (
        <NavLink key={chat.id} to={`/user/chats/${chat.id}`} className="flex bg-white text-black rounded-md px-3 py-1 cursor-pointer hover:bg-gray-200 !text-black">
          {chat.name}
        </NavLink>
      ))}
    </div>
    <div><Outlet /></div>
    </>
  )
}

export default ChatList;