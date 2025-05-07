import React from 'react';
import styles from './MainLayout.module.css';
import menuIcon from '../../assets/menu.svg';

interface MainLayoutProps {
  children?: React.ReactNode;
  userEmail: string;
  onLogout?: () => void;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children, userEmail, onLogout }) => {
  return (
    <div className={styles.container}>
      <header className={styles.header}>
        <img src={menuIcon} alt="Menu" className={styles.menuIcon} />
        <h1 className={styles.title}>Home</h1>
        <div className={styles.rightSection}>
          <span className={styles.email}>{userEmail}</span>
          <div className={styles.avatar} />
          <button className={styles.logoutBtn} onClick={onLogout}>Logout</button>
        </div>
      </header>

      <main className={styles.mainContent}>{children}</main>

      <footer className={styles.footer}>
        <div className={styles.footerLinks}>
            <span>©CPRMS 2025</span>
            <a href="#">Help</a>
            <a href="#">Contact</a>
        </div>
        
        <select className={styles.language}>
          <option>English (United States)</option>
          <option>Tiếng Việt</option>
        </select>
      </footer>
    </div>
  );
};

export default MainLayout;
