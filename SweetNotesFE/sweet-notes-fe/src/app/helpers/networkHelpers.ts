import { GraphQLClient as GQLClient } from 'graphql-request';

import { IFetchPost, IFetchGet } from '@/interfaces';

const rootUrl = process.env.NEXT_PUBLIC_BACKEND_ROOT_URI;

export const FetchPost = async ({
  route,
  body,
  fetchOptions = {},
}: IFetchPost): Promise<Response> => {
  return await fetch(`${rootUrl}/${route}`, {
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
  fetchOptions = {},
  headers = {},
}: IFetchGet): Promise<Response> => {
  return await fetch(`${rootUrl}/${route}`, {
    method: 'GET',
    mode: 'cors',
    cache: 'force-cache',
    headers: {
      'Content-Type': 'application/json',
      ...headers,
    },
    ...fetchOptions,
  });
};

export const GraphQLClient = new GQLClient(`${rootUrl}/graphql`, {
  credentials: `include`,
  mode: `cors`,
});
