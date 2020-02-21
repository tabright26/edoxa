import { Cookies } from "react-cookie";
import {
  getHomePath,
  getDefaultPath,
  getWorkflowStepsPath,
  getChallengesPath
} from "utils/coreui/constants";
import { Dispatch } from "react";
import { push } from "connected-react-router";

function infiniteExpires(): Date {
  const date = new Date();
  date.setFullYear(date.getFullYear() + 100);
  return date;
}

const EDOXA_REGISTER_VISITED = "EDOXA_REGISTER_VISITED";
export function isRegisterVisited(cookies: Cookies): boolean {
  return cookies.get(EDOXA_REGISTER_VISITED);
}
export function setRegisterVisited(cookies: Cookies): void {
  cookies.set(EDOXA_REGISTER_VISITED, true, {
    path: getHomePath(),
    expires: infiniteExpires()
  });
}

const EDOXA_WORKFLOW_STEP = "EDOXA_WORKFLOW_STEP";
export function redirectToWorkflow(
  cookies: Cookies,
  dispatch: Dispatch<any>
): void {
  if (hasWorkflowUnderWay(cookies)) {
    const step = getWorkflowStep(cookies);
    dispatch(push(getWorkflowStepsPath(step)));
  }
}
export function getWorkflowStep(cookies: Cookies): number | null {
  return cookies.get(EDOXA_WORKFLOW_STEP);
}
export function hasWorkflowUnderWay(cookies: Cookies): boolean {
  return !!getWorkflowStep(cookies);
}
export function nextWorkflowStep(
  cookies: Cookies,
  dispatch: Dispatch<any>,
  stepCount: number
): void {
  if (hasWorkflowUnderWay(cookies)) {
    var step = Number(cookies.get(EDOXA_WORKFLOW_STEP)) + 1;
    if (step >= stepCount) {
      removeWorkflowStep(cookies, dispatch);
    } else {
      setWorkflowStep(cookies, step);
      dispatch(push(getWorkflowStepsPath(step)));
    }
  }
}
export function setWorkflowStep(cookies: Cookies, stage: number): void {
  cookies.set(EDOXA_WORKFLOW_STEP, stage, {
    path: getDefaultPath(),
    expires: infiniteExpires()
  });
}
function removeWorkflowStep(cookies: Cookies, dispatch: Dispatch<any>): void {
  cookies.remove(EDOXA_WORKFLOW_STEP, {
    path: getDefaultPath()
  });
  dispatch(push(getChallengesPath()));
}
