import React, { useState, useEffect, Fragment } from "react";
import { Row, Col, Card, CardHeader, Button } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import { connectClans } from "store/organizations/clans/container";

import ClanItem from "components/Organizations/Clans/ClanItem";
import UserCandidatures from "components/Organizations/Candidatures/UserCandidatures";

import ClanModal from "modals/Organizations/Clan";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClansIndex = ({ actions, clans, userId }) => {
  const [clanList, setClanList] = useState(null);
  const [userClanId, setUserClanId] = useState(null);

  const [searchValue, setSearchValue] = useState("");
  const [sortValue, setSortValue] = useState("");

  useEffect(() => {
    actions.loadClans();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (clans) {
      var memberList = [];

      clans.forEach(clan => {
        clan.members.forEach(member => memberList.push(member));
      });

      memberList.forEach(member => {
        if (member.userId === userId) {
          setUserClanId(member.clanId);
          console.log(member.clanId);
        }
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clans]);

  useEffect(() => {
    if (clans) {
      var tempClans = clans.filter(clan => (searchValue ? clan.name.includes(searchValue) : clan));
      switch (sortValue) {
        case "byNameAsc":
          tempClans = tempClans.sort((clan1, clan2) => (clan1.name > clan2.name ? 1 : -1));
          console.log(tempClans);
          break;

        case "byNameDes":
          tempClans = tempClans.sort((clan1, clan2) => (clan1.name < clan2.name ? 1 : -1));
          console.log(tempClans);
          break;

        case "byMemberCountAsc":
          tempClans = tempClans.sort((clan1, clan2) => clan1.members.length - clan2.members.length);
          console.log(tempClans);
          break;

        case "byMemberCountDsc":
          tempClans = tempClans.sort((clan1, clan2) => clan2.members.length - clan1.members.length);
          console.log(tempClans);
          break;

        default:
          break;
      }
      setClanList(tempClans);
    }
  }, [clans, sortValue, searchValue]);

  const handleSearchInputChanges = e => {
    setSearchValue(e.target.value);
  };

  const handleSortInputChanges = e => {
    setSortValue(e.target.value);
  };

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          {!userClanId ? (
            <UserCandidatures userId={userId} />
          ) : (
            <LinkContainer to={"/structures/clans/" + userClanId + "/dashboard"}>
              <Button color="primary">Your dashboard</Button>
            </LinkContainer>
          )}
          <Card>
            <CardHeader>
              <Row>
                <Col>Clans</Col>
                <Col>
                  {clans.length} clans and counting...
                  {!userClanId ? (
                    <Fragment>
                      <div className="card-header-actions btn-link" onClick={() => actions.showCreateAddressModal()}>
                        or create your own
                      </div>
                      <ClanModal.Create actions={actions}></ClanModal.Create>
                    </Fragment>
                  ) : (
                    ""
                  )}
                </Col>
                <Col>
                  Search
                  <br />
                  <input type="text" value={searchValue} onChange={handleSearchInputChanges} />
                </Col>
                <Col>
                  Sorting
                  <br />
                  <select value={sortValue} onChange={handleSortInputChanges}>
                    <option value="noSort"></option>
                    <option value="byNameAsc">by name ascending</option>
                    <option value="byNameDes">by name descending</option>
                    <option value="byMemberCountAsc">by member count ascending</option>
                    <option value="byMemberCountDsc">by member count descending</option>
                  </select>
                </Col>
              </Row>
            </CardHeader>
          </Card>
        </Col>
      </Row>
      <Row>
        {clanList
          ? clanList.map((clan, index) => (
              <Col key={index} xs="6" sm="4" md="3">
                <ClanItem clan={clan} />
              </Col>
            ))
          : ""}
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClansIndex);
