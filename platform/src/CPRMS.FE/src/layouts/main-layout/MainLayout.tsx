import React from 'react';
import menuIcon from '../../assets/menu.svg';

interface MainLayoutProps {
  children?: React.ReactNode;
  userEmail: string;
  onLogout?: () => void;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children, userEmail, onLogout }) => {
  return (
    <div className="flex flex-col min-h-screen bg-white">
      <header className="flex items-center justify-between p-4 border-b border-gray-300 bg-white">
        <img src={menuIcon} alt="Menu" className="w-6 h-6" />
        <h1 className="flex-1 text-xl text-center">Home</h1>
        <div className="flex items-center gap-4">
          <span className="text-sm">{userEmail}</span>
          <div className="w-8 h-8 bg-gray-300 rounded-full" />
          <button
            className="px-4 py-2 border border-gray-300 rounded-lg bg-white hover:bg-gray-100"
            onClick={onLogout}
          >
            Logout
          </button>
        </div>
      </header>

      <main className="flex-grow p-6 bg-gray-50">{children}</main>

      <footer className="flex justify-between items-center p-6 border-t border-gray-300 bg-white text-sm">
        <div className="flex gap-4">
          <span>© CPRMS 2025</span>
          <a href="#" className="text-blue-500 hover:underline">Help</a>
          <a href="#" className="text-blue-500 hover:underline">Contact</a>
        </div>

        <select className="bg-transparent border-none cursor-pointer text-sm">
          <option>English (United States)</option>
          <option>Tiếng Việt</option>
        </select>
      </footer>
    </div>
  );
};

export default MainLayout;
