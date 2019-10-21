import React, { FunctionComponent } from "react";
import { Card, CardBody, CardHeader } from "reactstrap";

import { withCandidatures } from "store/root/organizations/candidatures/container";

import Item from "./Item/Item";

const CandidatureList: FunctionComponent<any> = ({ actions, candidatures, type, isOwner = false }) => (
  <Card>
    <CardHeader>Candidatures</CardHeader>
    <CardBody>{candidatures && candidatures.map((candidature, index: number) => <Item key={index} actions={actions} candidature={candidature} type={type} isOwner={isOwner}></Item>)}</CardBody>
  </Card>
);
export default withCandidatures(CandidatureList);
