import { LegacyRef } from 'react';

export interface IAddSpecialSomeone {
  buttonRef: LegacyRef<HTMLButtonElement> | undefined;
  specialSomeoneRefetch: () => {};
}
