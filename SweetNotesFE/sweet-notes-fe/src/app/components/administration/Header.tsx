import { useEffect, useState } from 'react';
import Avatar from 'boring-avatars';

import { Me } from '@/types';
import { Navigation } from '@/component/administration/Navigation';

import styles from './Header.module.scss';

export function Header({ me }: { me: Me }): JSX.Element {
  const [avatarSize, setAvatarSize] = useState<number>(90);

  useEffect(() => {
    if (typeof window !== `undefined`) {
      if (window.innerWidth >= 1186) setAvatarSize(140);
    }
  }, []);

  return (
    <header className={styles.header}>
      <Navigation />
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
