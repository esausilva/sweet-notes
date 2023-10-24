import { Inter } from 'next/font/google';

import './MainLayout.scss';

const inter = Inter({ subsets: ['latin'] });

export function MainLayout({ children }: { children: React.ReactNode }) {
  return <main className={inter.className}>{children}</main>;
}
