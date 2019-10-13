import { routerMiddleware } from "connected-react-router";

import { history } from "./constants";

export const middleware = routerMiddleware(history);
