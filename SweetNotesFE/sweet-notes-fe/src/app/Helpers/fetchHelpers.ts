import { ObjectLiteral } from '../../pages/types';

const rootUrl = process.env.NEXT_PUBLIC_BACKEND_ROOT_URI;

export const fetchPost = async (
  route: string,
  body: ObjectLiteral,
  fetchOptions: ObjectLiteral = {},
): Promise<Response> => {
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
