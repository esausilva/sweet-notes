import { SpecialSomeoneQuery } from '@/gql/graphql';

export function GetSpecialSomeoneId(
  data: SpecialSomeoneQuery,
  uniqueIdentifier: string,
): string {
  return data.specialSomeonesForUser.find(
    ss => ss.uniqueIdentifier === uniqueIdentifier,
  )?.id!;
}
