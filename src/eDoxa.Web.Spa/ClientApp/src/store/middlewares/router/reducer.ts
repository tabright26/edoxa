import { connectRouter } from "connected-react-router";

import { history } from "./constants";

export const reducer = connectRouter(history);
