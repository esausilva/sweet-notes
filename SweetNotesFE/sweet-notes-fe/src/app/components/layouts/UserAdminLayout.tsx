import { Inter } from 'next/font/google';

import { IAdminLayout } from '@/interfaces';

import './UserAdminLayout.scss';

const inter = Inter({ subsets: ['latin'] });

export function UserAdminLayout({
  children,
  header,
}: IAdminLayout): JSX.Element {
  return (
    <div className={`${inter.className} wrapper__main-admin`}>
      {header}
      <main className="main-admin">{children}</main>
    </div>
  );
}
