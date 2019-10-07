import React, { Fragment } from "react";
import { Row } from "reactstrap";

const ClanInfo = ({ clan }) => {
  return (
    <Fragment>
      <Row>Name: {clan ? clan.name : ""}</Row>
      <Row>Summary: {clan ? clan.summary : ""}</Row>
    </Fragment>
  );
};

export default ClanInfo;
