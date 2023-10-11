import { FormEvent, useReducer } from 'react';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '../app/MainLayout';
import { IFormFieldData, ILoginForm } from './interfaces';

import styles from './index.module.scss';

const initialFormState: ILoginForm = {
  email: '',
  password: '',
};

const formReducer = (
  state: ILoginForm,
  { name, value }: IFormFieldData,
): ILoginForm => {
  return {
    ...state,
    [name]: value,
  };
};

export default function Index(): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (
    event: FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    // const res = await fetch('http://localhost:5068/user/login', {
    //   method: 'POST',
    //   mode: 'cors',
    //   cache: 'no-store',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   },
    //   body: JSON.stringify({}),
    // });

    // console.log(res);
    console.log(formData);
  };

  return (
    <MainLayout>
      <Head>
        <title>Login | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Login</h1>

          <label htmlFor="email">Email</label>
          <input type="text" name="email" onChange={handleChange} />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" onChange={handleChange} />

          <button type="submit">Login</button>
        </form>
        <Link href="/signup">Need an account?</Link>
      </section>
    </MainLayout>
  );
}
