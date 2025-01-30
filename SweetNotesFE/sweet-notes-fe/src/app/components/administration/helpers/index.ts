import { SpecialSomeoneQuery } from '@/gql/graphql';

export function GetSpecialSomeoneId(
  data: SpecialSomeoneQuery,
  uniqueIdentifier: string,
): string {
  return data?.specialSomeonesForUser.find(
    ss => ss.uniqueIdentifier === uniqueIdentifier,
  )?.id!;
}

export function IsDoneLoadingSpecialSomeones(
  isLoading: boolean,
  data: SpecialSomeoneQuery,
): boolean {
  return isLoading === false && data?.specialSomeonesForUser.length! > 0;
}

export function FromUtcToLocal(utcDate: string) {
  return new Date(utcDate).toLocaleString('en-US', {
    year: 'numeric',
    month: 'numeric',
    day: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
  });
}

export function GetFirstAndLastDaysOfTheMonth(date: Date): {
  firstDay: String;
  lastDay: String;
} {
  const year = date!.getFullYear();
  const month = date!.getMonth();
  const firstDay = new Date(year, month, 1);
  const lastDay = new Date(year, month + 1, 0);
  lastDay.setHours(23, 59, 59);

  // NOTE: Might not need to convert to UTC
  return {
    firstDay: firstDay.toISOString(),
    lastDay: lastDay.toISOString(),
  };
}
