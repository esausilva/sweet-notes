import { ObjectLiteral } from '@/types';
import { Routes } from '@/constants';

interface IFetchBase {
  route: Routes;
  fetchOptions?: ObjectLiteral;
}

export interface IFetchPost extends IFetchBase {
  body: ObjectLiteral;
}

export interface IFetchGet extends IFetchBase {
  headers?: ObjectLiteral;
}
