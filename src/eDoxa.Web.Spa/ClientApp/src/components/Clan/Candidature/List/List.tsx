import React, { FunctionComponent } from "react";
import { Card, CardBody, CardHeader } from "reactstrap";
import { withCandidatures } from "store/root/organization/candidature/container";
import Item from "./Item/Item";
import { Candidature } from "types";
import Loading from "components/Shared/Loading";

const CandidatureList: FunctionComponent<any> = ({ actions, candidatures: { data, loading }, type, isOwner = false }) => (
  <Card>
    <CardHeader>Candidatures</CardHeader>
    <CardBody>
      {loading ? <Loading /> : data && data.map((candidature: Candidature, index: number) => <Item key={index} actions={actions} candidature={candidature} type={type} isOwner={isOwner}></Item>)}
    </CardBody>
  </Card>
);
export default withCandidatures(CandidatureList);
