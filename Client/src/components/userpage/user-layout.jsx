import { Outlet } from "react-router-dom";
import SidebarLink from "./sidebar-link"
import { logout } from '../../services/requests';
import logoutIcon from '../../assets/logout.svg';
import chatsIcon from '../../assets/chat-icon.svg';
import feedIcon from '../../assets/feed-icon.svg';
import settingsIcon from '../../assets/settings-icon.svg';

function UserLayout() {
  const navItems = [
    {
      to: "/user/chats",
      icon: chatsIcon,
      label: "chats",
      top: "top-20",
    },
    {
      to: "/user/feed",
      icon: feedIcon,
      label: "Feed",
      top: "top-30",
    },
    {
      to: "/user/settings",
      icon: settingsIcon,
      label: "Settings",
      top: "top-40",
    }
  ];

  return (
    <div className="grid grid-cols-3 grid-rows-2 h-screen grid-rows-[max-content]">
      <div></div>
      <div></div>
      <div>
        <a onClick={() => logout()} className="flex bg-white rounded-md p-1 cursor-pointer hover:bg-gray-200 w-[40%]">
          <img src={logoutIcon} className="w-[3vw] mr-1" alt="logout" />
          <span className="text-[2vw] text-black! text-white">Logout</span>
        </a>
      </div>
      <div>
        {navItems.map((item) => (
          <SidebarLink key={item.to} to={item.to} icon={item.icon} label={item.label} top={item.top} />
        ))}
      </div>
      <Outlet />
    </div>
  );
}

export default UserLayout;