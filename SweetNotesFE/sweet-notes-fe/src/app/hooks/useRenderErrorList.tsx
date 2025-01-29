import { JSX } from 'react';

import { ApiError } from '@/types';

export function useRenderErrorList(errors: ApiError): JSX.Element {
  const errorKeys = Object.keys(errors);

  if (errorKeys.length > 0) {
    var errorList = errorKeys.map(key => errors[key]).flat();
    return (
      <div className="error-list">
        <ul>
          {errorList.map((error, idx) => (
            <li key={`error-${idx}`}>{error}</li>
          ))}
        </ul>
      </div>
    );
  } else return <></>;
}
