import { FormEvent, useReducer } from 'react';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '../app/MainLayout';
import { SignupForm, FormFieldData } from './types';

import styles from './index.module.scss';

const initialFormState: SignupForm = {
  firstName: '',
  lastName: '',
  email: '',
  password: '',
};

const formReducer = (
  state: SignupForm,
  { name, value }: FormFieldData,
): SignupForm => {
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
        <title>Create an Account | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Signup</h1>

          <label htmlFor="firstName">First Name</label>
          <input type="text" name="firstName" onChange={handleChange} />

          <label htmlFor="lastName">Last Name</label>
          <input type="text" name="lastName" onChange={handleChange} />

          <label htmlFor="email">Email</label>
          <input type="text" name="email" onChange={handleChange} />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" onChange={handleChange} />

          <button type="submit">Signup</button>
        </form>
        <Link href="/">Already have an account?</Link>
      </section>
    </MainLayout>
  );
}
