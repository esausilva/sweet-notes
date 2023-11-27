import { Inter } from 'next/font/google';

import './MainEntryLayout.scss';

const inter = Inter({ subsets: ['latin'] });

export function MainLayout({ children }: { children: React.ReactNode }) {
  return (
    <main className={`${inter.className} wrapper__main-entry`}>{children}</main>
  );
}
