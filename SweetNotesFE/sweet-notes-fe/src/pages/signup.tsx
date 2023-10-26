import { FormEvent, useReducer, useState } from 'react';
import { useRouter } from 'next/navigation';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '@/layout/MainLayout';
import { SignupForm, FormFieldData, ApiError } from '@/types';
import { authenticate } from '@/service/authService';
import { useRenderErrorList } from '@/hook/useRenderErrorList';
import { USER_ADMIN_ROUTE } from '@/constants';

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

export default function Signup(): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);
  const [errors, setErrors] = useState({} as ApiError);
  const router = useRouter();

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (
    event: FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    var result = await authenticate('user/signup', {
      firstName: formData.firstName,
      lastName: formData.lastName,
      emailAddress: formData.email,
      password: formData.password,
    });

    if (result.status === 200) router.push(USER_ADMIN_ROUTE);
    if (result.errors) setErrors(result.errors);
  };

  return (
    <MainLayout>
      <Head>
        <title>Create an Account | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Signup</h1>

          <>{useRenderErrorList(errors)}</>

          <label htmlFor="firstName">First Name</label>
          <input type="text" name="firstName" onChange={handleChange} />

          <label htmlFor="lastName">Last Name</label>
          <input type="text" name="lastName" onChange={handleChange} />

          <label htmlFor="email">Email</label>
          <input type="text" name="email" onChange={handleChange} />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" onChange={handleChange} />

          <button className="button--primary" type="submit">
            Signup
          </button>
        </form>
        <Link href="/">Already have an account?</Link>
      </section>
    </MainLayout>
  );
}
