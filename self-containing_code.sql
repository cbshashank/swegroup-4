-- this code is for making the controller code more self-containing
-- this code will find all of the values that the characteristics could take on
-- then we could list these values as question answer choices on the homepage

select color_flower
from plant
group by color_flower
order by asc;

select color_foliage
from plant
group by color_foliage
order by asc;

select color_fruit_seed
from plant
group by color_fruit_seed
order by asc;

select texture_foliage
from plant
group by texture_foliage
order by asc;

select shape
from plant
group by shape
order by asc;

select pattern
from plant
group by pattern
order by asc;

------------------
select us_state
from location
group by us_state
order by asc;

------------------
select type
from plant_type
group by type
order by asc;