import { SpecialSomeoneQuery } from '@/gql/graphql';
import { SpecialSomeoneQueryResult } from '@/types';

export function GetSpecialSomeoneId(
  data: SpecialSomeoneQuery,
  uniqueIdentifier: string,
): string {
  return data.specialSomeonesForUser.find(
    ss => ss.uniqueIdentifier === uniqueIdentifier,
  )?.id!;
}

export function IsDoneLoadingSpecialSomeones(
  queryResult: SpecialSomeoneQueryResult,
): boolean {
  return (
    queryResult.isLoading === false &&
    queryResult.data?.specialSomeonesForUser.length! > 0
  );
}
