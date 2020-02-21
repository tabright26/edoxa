import React, { FunctionComponent } from "react";
import Layout from "components/App/Layout";
import DoxatagPanel from "components/Service/Identity/Doxatag/Panel";
import { WorkflowProps } from "views/Workflow";

const Step0: FunctionComponent<WorkflowProps> = ({ nextWorkflowStep }) => (
  <Layout.None>
    <DoxatagPanel nextWorkflowStep={nextWorkflowStep} />
  </Layout.None>
);

export default Step0;
