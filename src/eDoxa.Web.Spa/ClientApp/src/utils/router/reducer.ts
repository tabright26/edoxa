import { connectRouter } from "connected-react-router";

import { history } from "utils/router/history";

export const reducer = connectRouter(history);
