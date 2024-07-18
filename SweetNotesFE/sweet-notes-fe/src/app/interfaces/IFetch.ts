import { ObjectLiteral } from '@/types';
import { Routes, ApiRootUris } from '@/constants';

interface IFetchBase {
  route: Routes | string;
  rootUri?: ApiRootUris;
  fetchOptions?: ObjectLiteral;
}

export interface IFetchPost extends IFetchBase {
  body: ObjectLiteral;
}

export interface IFetchGet extends IFetchBase {
  headers?: ObjectLiteral;
}
