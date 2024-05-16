# FoodRecommendation
Food recommendation app

## Developers
- Paško Stipandžija
- Matej Copić

## Introduction
ShakFood is a web application for personalized restaurant and recipe recommendations.
With our web application, utilizing AI, you can easily discover restaurant dishes and recipes based on your cravings or items in your fridge.

## Features

### Recommending restaraunts
  - AI chat <br> 
      &#x25E6; Text based asnwer<br>
      &#x25E6; Text based restrictions
  - Sort management (reviews) <br>
      &#x25E6; Price and review sorting
### Recommending recipes
  - AI chat <br>
      &#x25E6; Text based asnwer<br>
      &#x25E6; Text based restrictions
  - Optimize ingredients and time

## Database
![ShakFood_DB_Model](https://github.com/OSS-Csharp-Seminar/FoodRecommendation/assets/92452548/17c8e00b-ee23-4d61-b481-bad5be50c6af)

## Llama

### Useful website for datasets
> [!IMPORTANT]
> Usefulness may vary depending on case
  - https://www.kaggle.com/datasets
  - https://sigma.ai/open-datasets/
  - https://www.openml.org

### Code used for preprocessing and formating data
> [!IMPORTANT]
> Provided code is an example and will most likey require major changes for adapting to each case
```
# load 
df_pp = pd.read_csv("PP_recipes.csv")
df_raw = pd.read_csv("RAW_recipes.csv")

# merge
merged_df = pd.merge(df_raw, df_pp[['id', 'ingredient_ids']], on='id', how='left')

# clean 
def clean_ingredient_ids(ids_str):
    if isinstance(ids_str, str):
        ids_list = ids_str.strip('[]').split(',')
        return [int(id_) for id_ in ids_list]
    else:
        return []

merged_df['ingredient_ids'] = merged_df['ingredient_ids'].apply(clean_ingredient_ids)

#filter
cols_to_remove = ['contributor_id', 'submitted', 'tags', 'n_steps', 'description', 'n_ingredients', 'ingredient_ids', 'id']
merged_df = merged_df.drop(columns=cols_to_remove)

def add_units(nutrition_values):
    units = [' calories', ' total fat', ' sugar', ' sodium', ' protein', ' saturated fat']
    nutrition_values = nutrition_values.strip("[]").split(", ")
    nutrition_with_units = [f"{value.strip()} {unit}" for value, unit in zip(nutrition_values, units)]
    return nutrition_with_units

merged_df['nutrition'] = merged_df['nutrition'].apply(add_units)

# Save 
merged_df.to_csv("merged_recipes.csv", index=False)
```
```
#load
df = pd.read_csv("merged_recipes.csv")

#format
def format_instructions(name, ingredients, minutes, nutrition, steps):
    ingredients_str = ", ".join(ingredient.strip("'[]") for ingredient in ingredients.split(", "))
    db_query = f"SELECT * FROM recipes WHERE ingredients LIKE '%{ingredients_str}%'"
    system_prompt = ""
    formatted_instructions = f"\n<s> [INST] <<SYS>>\n{system_prompt}\n<</SYS>>\n\n{ingredients_str} [/INST] {name}\n{minutes}\n{nutrition}\n{steps}\n{db_query} </s>"
    return formatted_instructions

df['text'] = df.apply(lambda row: format_instructions(row['name'], row['ingredients'], row['minutes'], row['nutrition'], row['steps']), axis=1)

#save
df.to_csv("formated_dataset_final.csv", index=False)
```
