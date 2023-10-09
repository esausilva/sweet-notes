import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '../app/MainLayout';

import styles from './index.module.scss';

export default function Index() {
  return (
    <MainLayout>
      <Head>
        <title>Create an Account | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm}>
          <h1>Signup</h1>

          <label htmlFor="firstName">First Name</label>
          <input type="text" name="firstName" />

          <label htmlFor="lastName">Last Name</label>
          <input type="text" name="lastName" />

          <label htmlFor="email">Email</label>
          <input type="text" name="email" />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" />

          <button>Signup</button>
        </form>
        <Link href="/">Already have an account?</Link>
      </section>
    </MainLayout>
  );
}
