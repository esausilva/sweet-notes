import { GraphQLClient as GQLClient } from 'graphql-request';

export const GraphQLClient = new GQLClient(
  `${process.env.NEXT_PUBLIC_BACKEND_ROOT_URI}/graphql`,
  {
    credentials: `include`,
    mode: `cors`,
  },
);
