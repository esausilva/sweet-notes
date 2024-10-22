import { useEffect, useState } from 'react';
import Link from 'next/link';
import Avatar from 'boring-avatars';

import { Me } from '@/types';
import { HamburgerIcon } from '@/resources/HamburgerIcon';
import { Routes } from '@/constants';

import styles from './Header.module.scss';

export function Header({ me }: { me: Me }): JSX.Element {
  const [avatarSize, setAvatarSize] = useState<number>(90);
  const [isNavOpen, setIsNavOpen] = useState<boolean>(false);

  useEffect(() => {
    if (typeof window !== `undefined`) {
      if (window.innerWidth >= 1186) setAvatarSize(140);
    }
  }, []);

  const toggleNavigation = () => setIsNavOpen(!isNavOpen);

  return (
    <header className={styles.header}>
      <HamburgerIcon className={styles.hamburger} onClick={toggleNavigation} />
      <nav className={`${styles.navigation} ${isNavOpen ? '' : styles.closed}`}>
        <button
          aria-label="Close Navigation"
          name="Close Navigation"
          onClick={toggleNavigation}
        >
          &times;
        </button>
        <ul className={styles.navigationItems}>
          <li>
            <Link href={`/${Routes.USER_ADMINISTRATION}`}>Administration</Link>
          </li>
          <li>
            <a href="#">Update Password</a>
          </li>
          <li>
            <Link
              href={`${process.env.NEXT_PUBLIC_API_CLIENT_SIDE_ROOT_URI}/${Routes.USER_LOGOUT}`}
            >
              Logout
            </Link>
          </li>
        </ul>
      </nav>
      <h1 className={styles.title}>Sweet Notes</h1>
      <div className={styles.avatar}>
        <p>{me.name}</p>
        <Avatar
          size={avatarSize}
          name={me.name}
          variant="beam"
          colors={['#73C5AA', '#C6C085', '#F9A177', '#F76157', '#C1AB8B']}
        />
      </div>
    </header>
  );
}
