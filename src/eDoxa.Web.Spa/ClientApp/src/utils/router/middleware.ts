import { routerMiddleware } from "connected-react-router";

import { history } from "utils/router/config";

export const middleware = routerMiddleware(history);
