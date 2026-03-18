import { Outlet, useNavigate, NavLink } from "react-router-dom";
import { logout } from '../../services/requests';
import logoutIcon from '../../assets/logout.svg';
import chatsIcon from '../../assets/chat-icon.svg';
import feedIcon from '../../assets/feed-icon.svg';
import settingsIcon from '../../assets/settings-icon.svg';

function UserLayout() {
  const navigate = useNavigate();

  return (
    <div>
      <button onClick={() => logout()} className="fixed top-5 right-5 bg-white! flex items-center justify-center gap-[0.5vw] bg-red-500 text-white p-[0.5vw] rounded-md transition-all">
        <img src={logoutIcon} className="w-[3vw] mr-1" alt="logout" />
        <span className="text-[2vw] text-black! text-white">Logout</span>
      </button>

      <NavLink to="/user/chats" className={({ isActive }) =>
            `fixed top-20 left-5 w-[35%] flex items-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all ${
            isActive ? "bg-blue-500 text-white" : "bg-red-500 text-white"
            }`
        }>
        <img src={chatsIcon} className="w-[3vw] mr-1" alt="chats" />
        <span className="text-[2vw] text-black! text-white">Chats</span>
      </NavLink>

      <NavLink to="/user/feed" className={({ isActive }) =>
        `fixed top-30 left-5 w-[35%] flex items-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all ${
        isActive ? "bg-blue-500 text-white" : "bg-red-500 text-white"
        }`
        }>
        <img src={feedIcon} className="w-[3vw] mr-1" alt="feed" />
        <span className="text-[2vw] text-black! text-white">Feed</span>
      </NavLink>

      <NavLink to="/user/settings" className={({ isActive }) =>
        `fixed top-40 left-5 w-[35%] flex items-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all ${
        isActive ? "bg-blue-500 text-white" : "bg-red-500 text-white"
        }`
        }>
        <img src={settingsIcon} className="w-[3vw] mr-1" alt="Publications" />
        <span className="text-[2vw] text-black! text-white">Settings</span>
      </NavLink>

      <div className="ml-[40%] p-4">
        <Outlet />
      </div>
    </div>
  );
}

export default UserLayout;