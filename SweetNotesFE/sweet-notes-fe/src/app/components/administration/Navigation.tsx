import { JSX, useState } from 'react';
import Link from 'next/link';

import { HamburgerIcon } from '@/resources/HamburgerIcon';
import { Routes } from '@/constants';

import styles from './Navigation.module.scss';

export function Navigation(): JSX.Element {
  const [isNavOpen, setIsNavOpen] = useState<boolean>(false);

  const toggleNavigation = () => setIsNavOpen(!isNavOpen);

  return (
    <>
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
            <a href={`/${Routes.USER_UPDATE_PASSWORD}`}>Update Password</a>
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
    </>
  );
}
