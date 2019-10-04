import React, { useState, useEffect } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import { connectClans } from "store/organizations/clans/container";

import ClanInfo from "./ClanInfo";
import Candidatures from "../Candidatures/Candidatures";
import Members from "../ClanMembers/Members";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClanDashboardIndex = ({
  actions,
  clans,
  match: {
    params: { clanId }
  }
}) => {
  const [clan, setClan] = useState(null);

  useEffect(() => {
    if (!clans.some(clan => clan.id === clanId)) {
      actions.loadClan(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    setClan(clans.find(clan => clan.id === clanId));
  }, [clanId, clans]);

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h3" className="text-center">
              {clan ? clan.name : "Clan"} Dashboard: Clan Id: {clanId}
            </CardHeader>
            <CardBody>
              <Row>
                <Col xs="4" sm="4" md="4">
                  {clan ? <ClanInfo clanId={clan.id} /> : ""}
                </Col>
                <Col xs="6" sm="6" md="6">
                  {clan ? <Members clanId={clan.id} /> : ""}
                </Col>
                <Col xs="2" sm="2" md="2">
                  {clan ? <Candidatures clanId={clan.id} /> : ""}
                </Col>
              </Row>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClanDashboardIndex);
