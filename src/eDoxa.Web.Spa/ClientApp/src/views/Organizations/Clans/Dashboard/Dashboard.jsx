import React, { useState, useEffect } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import { connectClans } from "store/organizations/clans/container";

import ClanInfo from "components/Organizations/Clans/ClanInfo";
import ClanLogo from "components/Organizations/Clans/ClanLogo";
import ClanCandidatures from "components/Organizations/Candidatures/ClanCandidatures";
import ClanInvitations from "components/Organizations/Invitations/ClanInvitations";
import InvitationWidget from "components/Organizations/Invitations/InvitationWidget";
import Members from "components/Organizations/ClanMembers/Members";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClanDashboardIndex = ({
  actions,
  clans,
  userId,
  doxaTags,
  match: {
    params: { clanId }
  }
}) => {
  const [clan, setClan] = useState(null);
  const [isOwner, setIsOwner] = useState(null);

  useEffect(() => {
    if (!clans.some(clan => clan.id === clanId)) {
      actions.loadClan(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    if (clans) {
      setClan(clans.find(clan => clan.id === clanId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId, clans]);

  useEffect(() => {
    console.log("sdfddf");
    if (clan) {
      setIsOwner(clan.ownerId === userId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan, userId]);

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card>
            <CardHeader>
              <Col>{clan ? clan.name : ""} Dashboard</Col>
              <Col>Clan Id: {clan ? clan.id : ""}</Col>
              <Col>Clan Owner Id: {clan ? clan.ownerId : ""}</Col>
              {isOwner ? (
                <Col>
                  <InvitationWidget clanId={clan.id} />
                </Col>
              ) : (
                ""
              )}
            </CardHeader>
            <CardBody>
              <Row>
                <Col>
                  <Card>
                    <CardHeader>{clan ? <ClanLogo clanId={clan.id} userId={userId} isOwner={isOwner} /> : ""}</CardHeader>
                    <CardBody>{clan ? <ClanInfo clan={clan} /> : ""}</CardBody>
                  </Card>
                </Col>
                <Col>{clan ? <Members clanId={clan.id} userId={userId} doxaTags={doxaTags} isOwner={isOwner} /> : ""}</Col>
                <Col>{clan ? <ClanCandidatures clan={clan.id} userId={userId} doxaTags={doxaTags} isOwner={isOwner} /> : ""}</Col>
                <Col>{clan ? <ClanInvitations clan={clan.id} userId={userId} doxaTags={doxaTags} isOwner={isOwner} /> : ""}</Col>
              </Row>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClanDashboardIndex);
