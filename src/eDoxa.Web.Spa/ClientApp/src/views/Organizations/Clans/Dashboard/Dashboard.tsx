import React, { useState, useEffect, FunctionComponent } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";
import { toastr } from "react-redux-toastr";

import { withClans } from "store/root/organizations/clans/container";

// import ClanLogo from "components/Organizations/Clans/ClanLogo";
import ClanInfo from "components/Organization/Clan/Summary/Summary";

import CandidatureList from "components/Organization/Clan/Candidature/List/List";
import InvitationList from "components/Organization/Clan/Invitation/List/List";

// import InvitationWidget from "components/Organizations/Invitations/InvitationWidget";

import Members from "components/Organization/Clan/Member/List/List";

import ErrorBoundary from "components/Shared/ErrorBoundary";

import { compose } from "recompose";

//USE LAZY LOADING

const ClanDashboardIndex: FunctionComponent<any> = ({
  actions,
  clans: { data },
  userId,
  match: {
    params: { clanId }
  }
}) => {
  const [clan, setClan] = useState(null);
  const [isOwner, setIsOwner] = useState(false);
  const [isMember, setIsMember] = useState(false);

  useEffect(() => {
    if (!data.some(clan => clan.id === clanId)) {
      actions.loadClan(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    setClan(data.find(clan => clan.id === clanId));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    if (clan) {
      setIsMember(clan.members.some(member => member.userId === userId));
      if (isMember) {
        setIsOwner(clan.ownerId === userId);
        if (isOwner) {
          toastr.success("SUCCESS", "Entered dashboard as Admin.");
        } else {
          toastr.success("SUCCESS", "Entered dashboard as member.");
        }
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          {isMember ? (
            <Card>
              <CardHeader>{/* {isOwner ? (
                  <Col>
                    <InvitationWidget clanId={clan.id} />
                  </Col>
                ) : null} */}</CardHeader>
              <CardBody>
                <Row>
                  <Col>
                    <Card>
                      <CardHeader>{/* <ClanLogo clanId={clan.id} userId={userId} isOwner={isOwner} /> */}</CardHeader>
                      <CardBody>
                        <ClanInfo clan={clan} />
                      </CardBody>
                    </Card>
                  </Col>
                  <Col>
                    <Members clanId={clan.id} userId={userId} isOwner={isOwner} />
                  </Col>
                  <Col>
                    <InvitationList type="clan" id={clan.id} />
                  </Col>
                  <Col>
                    <CandidatureList type="clan" id={clan.id} isOwner={isOwner} />
                  </Col>
                </Row>
              </CardBody>
            </Card>
          ) : null}
        </Col>
      </Row>
    </ErrorBoundary>
  );
};

const enhance = compose<any, any>(withClans);

export default enhance(ClanDashboardIndex);