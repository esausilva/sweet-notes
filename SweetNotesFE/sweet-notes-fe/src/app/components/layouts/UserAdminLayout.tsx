import { Inter } from 'next/font/google';

import { Header } from '@/component/administration/Header';
import { IAdminLayout } from '@/interfaces';

import './UserAdminLayout.scss';

const inter = Inter({ subsets: ['latin'] });

export function UserAdminLayout({ children, me }: IAdminLayout): JSX.Element {
  return (
    <div className={`${inter.className} wrapper__main-admin`}>
      <Header me={me} />
      <main>{children}</main>
    </div>
  );
}
