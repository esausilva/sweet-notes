import { GraphQLClient as GQLClient } from 'graphql-request';

import { IFetchPost, IFetchGet } from '@/interfaces';
import { ApiRootUris } from '@/constants';

const forClientSideRootUri = process.env.NEXT_PUBLIC_API_CLIENT_SIDE_ROOT_URI;
const forServerSideRootUri = process.env.NEXT_PUBLIC_API_SERVER_SIDE_ROOT_URI;

export const FetchPost = async ({
  route,
  body,
  rootUri = ApiRootUris.FOR_CLIENT_SIDE,
  fetchOptions = {},
}: IFetchPost): Promise<Response> => {
  const baseUri =
    rootUri === ApiRootUris.FOR_CLIENT_SIDE
      ? forClientSideRootUri
      : forServerSideRootUri;

  return await fetch(`${baseUri}/${route}`, {
    method: 'POST',
    mode: 'cors',
    cache: 'no-store',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
    ...fetchOptions,
  });
};

export const FetchGet = async ({
  route,
  rootUri = ApiRootUris.FOR_CLIENT_SIDE,
  fetchOptions = {},
  headers = {},
}: IFetchGet): Promise<Response> => {
  const baseUri =
    rootUri === ApiRootUris.FOR_CLIENT_SIDE
      ? forClientSideRootUri
      : forServerSideRootUri;

  return await fetch(`${baseUri}/${route}`, {
    method: 'GET',
    mode: 'cors',
    cache: 'no-store',
    headers: {
      'Content-Type': 'application/json',
      ...headers,
    },
    ...fetchOptions,
  });
};

export const GraphQLClient = new GQLClient(`${forClientSideRootUri}/graphql`, {
  credentials: `include`,
  mode: `cors`,
});
