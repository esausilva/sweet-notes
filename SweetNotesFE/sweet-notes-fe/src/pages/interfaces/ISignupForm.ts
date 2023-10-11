import { ILoginForm } from './ILoginForm';

export interface ISignupForm extends ILoginForm {
  firstName: string;
  lastName: string;
}
