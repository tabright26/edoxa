import React, { FunctionComponent } from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { nextWorkflowStep } from "utils/cookies/constants";
import { compose } from "recompose";
import { ReactCookieProps, withCookies } from "react-cookie";
import { MapDispatchToProps, connect } from "react-redux";

const Step0 = React.lazy(() => import("./Steps/0"));
const Step1 = React.lazy(() => import("./Steps/1"));

const steps = [Step0, Step1];

export type WorkflowProps = { nextWorkflowStep?: () => void };

type Params = { step: string };

type OwnProps = ReactCookieProps & RouteComponentProps<Params>;

type DispatchProps = {
  nextWorkflowStep: () => void;
};

type InnerProps = DispatchProps & OwnProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Workflow: FunctionComponent<Props> = ({ match, nextWorkflowStep }) => {
  const Step = steps[match.params.step];
  return <Step nextWorkflowStep={nextWorkflowStep} />;
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any,
  ownProps
) => {
  return {
    nextWorkflowStep: () => {
      nextWorkflowStep(ownProps.cookies, dispatch, steps.length);
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  withCookies,
  connect(null, mapDispatchToProps)
);

export default enhance(Workflow);
