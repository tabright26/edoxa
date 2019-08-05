import { createLogger } from 'redux-logger';

export function middleware() {
  return createLogger();
}
