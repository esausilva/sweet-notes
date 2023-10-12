import { LoginForm } from './LoginForm';

export type SignupForm = LoginForm & {
  firstName: string;
  lastName: string;
};
