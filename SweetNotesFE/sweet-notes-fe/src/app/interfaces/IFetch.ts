import { ObjectLiteral } from '@/types';
import { Routes } from '@/constants';

interface IFetchBase {
  route: Routes | string;
  fetchOptions?: ObjectLiteral;
}

export interface IFetchPost extends IFetchBase {
  body: ObjectLiteral;
}

export interface IFetchGet extends IFetchBase {
  headers?: ObjectLiteral;
}
