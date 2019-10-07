import React, { useEffect, useState } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import { connectClans } from "store/organizations/clans/container";

import ClanInfo from "./ClanInfo";
import CandidatureWidget from "../Candidatures/CandidatureWidget";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClanDetailsIndex = ({
  actions,
  clans,
  userId,
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
  }, [clanId, clans, clan, userId]);

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h3" className="text-center">
              <Row>
                <Col xs="6" sm="6" md="6">
                  Clan Details: Clan Id: {clanId}
                </Col>
                <Col xs="6" sm="6" md="6">
                  {clan ? <CandidatureWidget clan={clan} /> : ""}
                </Col>
              </Row>
            </CardHeader>
            <CardBody>
              <Row>
                <Col xs="12" sm="12" md="12">
                  {clan ? <ClanInfo clan={clan} /> : ""}
                </Col>
              </Row>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClanDetailsIndex);
