import { Cookies } from "react-cookie";
import { getHomePath } from "utils/coreui/constants";

const EDOXA_REGISTER_VISITED = "EDOXA_REGISTER_VISITED";

export function isRegisterVisited(cookies: Cookies): boolean {
  return cookies.get(EDOXA_REGISTER_VISITED);
}

export function setRegisterVisited(cookies: Cookies): void {
  const date = new Date();
  date.setFullYear(date.getFullYear() + 100);
  cookies.set(EDOXA_REGISTER_VISITED, true, {
    path: getHomePath(),
    expires: date
  });
}
