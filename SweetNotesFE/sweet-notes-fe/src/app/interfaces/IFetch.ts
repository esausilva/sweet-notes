import { ObjectLiteral } from '@/types';

interface IFetchBase {
  route: string;
  fetchOptions?: ObjectLiteral;
}

export interface IFetchPost extends IFetchBase {
  body: ObjectLiteral;
}

export interface IFetchGet extends IFetchBase {
  headers?: ObjectLiteral;
}
