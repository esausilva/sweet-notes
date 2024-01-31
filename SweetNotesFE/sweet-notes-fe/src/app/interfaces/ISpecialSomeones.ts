import { SpecialSomeoneQueryResult } from '@/types';

export interface ISpecialSomeones {
  children: React.ReactNode;
  queryResult: SpecialSomeoneQueryResult;
  specialSomeone: {
    uniqueIdentifier: string;
    id: string;
  };
}
