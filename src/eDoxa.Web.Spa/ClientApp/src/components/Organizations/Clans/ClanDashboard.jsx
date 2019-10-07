import React, { useState, useEffect } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import { connectClans } from "store/organizations/clans/container";

import ClanInfo from "./ClanInfo";
import ClanCandidatures from "../Candidatures/ClanCandidatures";
import ClanInvitations from "../Invitations/ClanInvitations";
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
                  {clan ? <ClanInfo clan={clan} /> : ""}
                </Col>
                <Col xs="3" sm="3" md="3">
                  {clan ? <Members clan={clan} /> : ""}
                </Col>
                <Col xs="3" sm="3" md="3">
                  {clan ? <ClanCandidatures clan={clan} /> : ""}
                </Col>
                <Col xs="2" sm="2" md="2">
                  {clan ? <ClanInvitations clan={clan} /> : ""}
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
