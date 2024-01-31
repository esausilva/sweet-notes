import { UseQueryResult } from '@tanstack/react-query';

import { SpecialSomeoneQuery } from '@/gql/graphql';

export type SpecialSomeoneQueryResult = UseQueryResult<
  SpecialSomeoneQuery,
  Error
>;
