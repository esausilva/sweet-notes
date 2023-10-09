import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '../app/MainLayout';

import styles from './index.module.scss';

export default function Index() {
  return (
    <MainLayout>
      <Head>
        <title>Login | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm}>
          <h1>Login</h1>

          <label htmlFor="email">Email</label>
          <input type="text" name="email" />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" />

          <button>Login</button>
        </form>
        <Link href="/signup">Need an account?</Link>
      </section>
    </MainLayout>
  );
}
